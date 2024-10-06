import { useNavigate } from 'react-router-dom';
import { useAuth } from '../../hooks/useAuth';
import { ReactNode } from 'react';
import { Role } from '../../types/types';

interface Props {
  children?: ReactNode;
  allowedRoles: Role[];
}

export const ProtectedRoute: React.FC<Props> = ({ children, allowedRoles }) => {
  const { role } = useAuth();
  const navigate = useNavigate();
  if (!allowedRoles.includes(role)) {
    navigate('/login');
  }
  return children;
};
