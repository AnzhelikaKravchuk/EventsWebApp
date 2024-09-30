import { useState } from 'react';
import NavBar from '../../components/NavBar/NavBar';
import { Outlet } from 'react-router-dom';
import { useAuth } from '../../hooks/useAuth';
import { Role } from '../../types/types';

export default function Root() {
  const { role } = useAuth();
  return (
    <main>
      <NavBar isAuthenticated={role !== Role.Guest} />
      <Outlet />
    </main>
  );
}
