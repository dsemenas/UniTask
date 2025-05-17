import React from 'react';

export default function HelloMessage() {
    const [message, setMessage] = useState('');

    useEffect(() => {
        fetch('http://localhost:5000/hello') // adjust port if needed
          .then(response => response.text())
          .then(data => setMessage(data))
          .catch(error => {
            console.error('Error fetching message:', error);
            setMessage('Failed to load message');
          });
      }, []);
    
}