xport const AuthProvider = ({ children }) => {
    const [token, setToken] = useState(localStorage.getItem("token"));
    const [user, setUser] = useState(null);
    const [loading, setLoading] = useState(true); // naujas loading state
  
    useEffect(() => {
      const fetchUser = async () => {
        if (!token) {
          setUser(null);
          setLoading(false);
          return;
        }
  
        try {
          const response = await fetch("http://localhost:5159/api/User/me", {
            method: "GET",
            headers: {
              Authorization: `Bearer ${token}`,
              "Content-Type": "application/json",
            },
          });
  
          if (response.ok) {
            const data = await response.json();
            setUser(data.data);
          } else {
            setUser(null);
          }
        } catch (error) {
          console.error("Klaida gaunant naudotoją:", error);
          setUser(null);
        }
        setLoading(false);
      };
  
      fetchUser();
    }, [token]);
  
    const login = (token) => {
      localStorage.setItem("token", token);
      setToken(token);
      setLoading(true); // kai naujas token, reikia vėl užkrauti userį
    };
  
    const logout = () => {
      localStorage.removeItem("token");
      setToken(null);
      setUser(null);
    };
  
    return (
      <AuthContext.Provider value={{ token, user, login, logout, loading }}>
        {children}
      </AuthContext.Provider>
    );
  };