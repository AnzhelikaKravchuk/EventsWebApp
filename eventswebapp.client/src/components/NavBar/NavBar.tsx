import {
  AppBar,
  Box,
  Button,
  Container,
  Toolbar,
  Typography,
} from '@mui/material';
import { useAuth } from '../../hooks/useAuth';
import { Role } from '../../types/types';

const authenticatedUserMenu = [
  {
    link: '/socialEvents',
    text: 'Events',
  },
  {
    link: '/admissions',
    text: 'Admissions',
  },
];

const unauthenticatedUserMenu = [
  {
    link: '/login',
    text: 'Login',
  },
  {
    link: '/register',
    text: 'Sign Up',
  },
];

export default function NavBar() {
  const { role, logout } = useAuth();

  const isAuthenticated = role !== Role.Guest;

  const buttonSx = {
    display: 'block',
    color: 'white',
    '&.active': {
      background: '#ffffff50',
      fontWeight: 'bold',
    },
  };

  return (
    <AppBar
      position='fixed'
      sx={{ zIndex: (theme) => theme.zIndex.drawer + 1 }}>
      <Container maxWidth='xl'>
        <Toolbar variant='dense' sx={{ my: 2 }}>
          <Box border={2} padding={0.5}>
            <Typography variant='h6'>Events App</Typography>
          </Box>
          <Box sx={{ ml: 2, flexGrow: 1, display: 'flex' }}>
            {isAuthenticated
              ? authenticatedUserMenu.map(({ link, text }) => (
                  <Button href={link} sx={buttonSx}>
                    {text}
                  </Button>
                ))
              : unauthenticatedUserMenu.map(({ link, text }) => (
                  <Button href={link} sx={buttonSx}>
                    {text}
                  </Button>
                ))}
          </Box>
          {isAuthenticated && (
            <Button onClick={logout} sx={buttonSx}>
              Logout
            </Button>
          )}
        </Toolbar>
      </Container>
    </AppBar>
  );
}
