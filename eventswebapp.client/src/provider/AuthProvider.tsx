import React, { createContext, useState, useEffect, ReactNode } from 'react';
import { Role } from '../types/types';
import { GetRole, Logout } from '../services/user';
import { useNavigate } from 'react-router-dom';

export const AuthContext = createContext({
  role: Role.Guest,
  logout: () => {},
  authenticate: () => {},
});

interface Props {
  children?: ReactNode;
}
export const AuthProvider: React.FC<Props> = ({ children }) => {
  const [role, setRole] = useState<Role | null>(null);
  const [loading, setLoading] = useState(false);
  const navigate = useNavigate();

  const logout = () => {
    setRole(Role.Guest);
    Logout();
    navigate('/login');
  };

  const authenticate = async () => {
    await updateRole();
  };

  const updateRole = () => {
    setLoading(true);
    return GetRole()
      .then((role) => {
        setRole(role);
      })
      .catch(() => {
        logout();
      })
      .finally(() => {
        setLoading(false);
      });
  };

  useEffect(() => {
    updateRole();
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  if (role === null) {
    return null;
  }

  const value = {
    role,
    logout,
    authenticate,
  };

  return (
    <AuthContext.Provider value={value}>
      {loading ? 'Loading' : children}
    </AuthContext.Provider>
  );
};
