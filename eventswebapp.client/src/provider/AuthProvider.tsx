import React, { createContext, useState, useEffect, ReactNode } from 'react';
import { Repository } from '../utils/Repository';
import { Role } from '../types/types';

export const AuthContext = createContext({
  role: Role.Guest,
  logout: () => {},
  login: () => {},
});

interface Props {
  children?: ReactNode;
}
export const AuthProvider: React.FC<Props> = ({ children }) => {
  const [role, setRole] = useState(Role.Guest);
  const [isAuthenticated, setIsAuthenticated] = useState(false);
  const [loading, setLoading] = useState(false);

  const logout = () => {
    setIsAuthenticated(false);
    setRole(Role.Guest);
  };

  useEffect(() => {
    console.log('Here2');
    if (isAuthenticated) {
      Repository.GetRole()
        .then((role) => {
          setRole(role);
        })
        .catch(() => {
          logout();
        });
    }
    setLoading(false);
  }, [isAuthenticated]);

  const value = {
    role,
    logout,
    login: () => {
      setLoading(true);
      setIsAuthenticated(true);
    },
  };
  return (
    <AuthContext.Provider value={value}>
      {loading ? 'Loading' : children}
    </AuthContext.Provider>
  );
};
