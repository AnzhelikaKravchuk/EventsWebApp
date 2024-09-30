import { Navigate } from 'react-router-dom';
import { useAuth } from '../../hooks/useAuth';
import { ReactNode } from 'react';
import { Role } from '../../types/types';

interface Props {
  children?: ReactNode;
  allowedRoles: Role[];
}

export const ProtectedRoute: React.FC<Props> = ({ children, allowedRoles }) => {
  const { role } = useAuth();

  if (!allowedRoles.includes(role)) {
    return <Navigate to='/login' />;
  }
  return children;
};
