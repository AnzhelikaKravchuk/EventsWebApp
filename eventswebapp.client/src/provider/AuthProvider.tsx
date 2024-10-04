import React, { createContext, useState, useEffect, ReactNode } from 'react';
import { Role } from '../types/types';
import { GetRole, Logout } from '../services/user';

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

  const logout = () => {
    setRole(Role.Guest);
    Logout();
  };

  const authenticate = async () => {
    await updateRole();
  };

  const updateRole = () => {
    setLoading(true);
    return GetRole()
      .then((role) => {
        console.log('Role', role);
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
