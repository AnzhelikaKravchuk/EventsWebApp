import { Box } from '@mui/material';
import NavBar from '../../components/NavBar/NavBar';
import { Outlet } from 'react-router-dom';

export default function Root() {
  return (
    <>
      <NavBar />
      <main>
        <Outlet />
      </main>
    </>
  );
}
