import React, {useState, useEffect} from 'react';
import axios from 'axios';
import { Redirect } from 'react-router';

const ChatPage = () => {
const [users, setUsers] = useState([]);
const [messages, setMessages] = useState([]);
    const [messageInput, setMessageInput] = useState('');
    const isAuthenticated = useState(localStorage.getItem('isAuthenticated'))
  
    useEffect(() => {
        if (!isAuthenticated) {
            <Redirect to='/login'/>
        }
    })
const getUsers = async () => {
  axios.get('api/users')
}
  return <div>Welcome to the chat page. Please register or login to participate. </div>;
};

export default ChatPage;
