import { useState, useEffect, useCallback } from 'react';
import { ref, query, orderByChild, limitToLast, onValue, push, DatabaseReference } from 'firebase/database';
import { db } from '../firebase';
import { Message } from '../types';

const MESSAGES_PER_PAGE = 50;

export function useFirebaseChat(isOpen: boolean) {
  const [messages, setMessages] = useState<Message[]>([]);
  const [loading, setLoading] = useState(true);
  const [unreadCount, setUnreadCount] = useState(0);
  const [lastMessageTimestamp, setLastMessageTimestamp] = useState<number | null>(null);

  // Cargar mensajes con paginación
  useEffect(() => {
    if (isOpen) {
      setLoading(true);
      const chatRef = ref(db, 'chat');
      const messagesQuery = query(
        chatRef,
        orderByChild('timestamp'),
        limitToLast(MESSAGES_PER_PAGE)
      );

      const unsubscribe = onValue(messagesQuery, (snapshot) => {
        const data = snapshot.val();
        if (data) {
          const messageList = Object.entries(data)
            .map(([id, value]: [string, any]) => ({
              id,
              ...value
            }))
            .sort((a, b) => a.timestamp - b.timestamp);

          setMessages(messageList);
          setLastMessageTimestamp(messageList[messageList.length - 1]?.timestamp || null);
        }
        setLoading(false);
      });

      return () => unsubscribe();
    } else {
      // Contar mensajes no leídos cuando el chat está cerrado
      const chatRef = ref(db, 'chat');
      const unsubscribe = onValue(chatRef, (snapshot) => {
        const data = snapshot.val();
        if (data) {
          const newMessages = Object.values(data).filter((msg: any) => 
            msg.timestamp > Date.now() - 300000 // últimos 5 minutos
          ).length;
          setUnreadCount(newMessages);
        }
      });

      return () => unsubscribe();
    }
  }, [isOpen]);

  // Función para enviar mensajes
  const sendMessage = useCallback(async (text: string, username: string, isAdmin: boolean) => {
    try {
      const chatRef = ref(db, 'chat');
      await push(chatRef, {
        text,
        timestamp: Date.now(),
        username,
        isAdmin
      });
      return true;
    } catch (error) {
      console.error('Error sending message:', error);
      return false;
    }
  }, []);

  // Función para cargar más mensajes antiguos
  const loadMoreMessages = useCallback(async () => {
    if (!lastMessageTimestamp) return;

    const chatRef = ref(db, 'chat');
    const oldMessagesQuery = query(
      chatRef,
      orderByChild('timestamp'),
      limitToLast(MESSAGES_PER_PAGE)
    );

    try {
      const snapshot = await new Promise<any>((resolve, reject) => {
        onValue(oldMessagesQuery, resolve, reject);
      });

      const data = snapshot.val();
      if (data) {
        const oldMessages = Object.entries(data)
          .map(([id, value]: [string, any]) => ({
            id,
            ...value
          }))
          .filter((msg) => msg.timestamp < lastMessageTimestamp)
          .sort((a, b) => a.timestamp - b.timestamp);

        setMessages((prev) => [...oldMessages, ...prev]);
        if (oldMessages.length > 0) {
          setLastMessageTimestamp(oldMessages[0].timestamp);
        }
      }
    } catch (error) {
      console.error('Error loading more messages:', error);
    }
  }, [lastMessageTimestamp]);

  return {
    messages,
    loading,
    unreadCount,
    sendMessage,
    loadMoreMessages
  };
}