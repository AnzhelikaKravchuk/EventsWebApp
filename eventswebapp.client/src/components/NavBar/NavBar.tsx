import { AppBar, Button, Toolbar, Typography } from '@mui/material';
import { NavLink } from 'react-router-dom';
import { useAuth } from '../../hooks/useAuth';
interface Props {
  isAuthenticated: boolean;
}

export default function NavBar(props: Props) {
  const { logout } = useAuth();
  return (
    <AppBar position='static'>
      <Toolbar variant='dense'>
        <Typography variant='h2'>Events Web Application</Typography>
        {props.isAuthenticated ? (
          <div>
            <NavLink to='/socialEvents'>Social Events</NavLink>
            <NavLink to='/admissions'>Your Admissions</NavLink>
            <Button onClick={logout}>Logout</Button>
          </div>
        ) : (
          <div>
            <NavLink to='/login'>Login</NavLink>
            <NavLink to='/register'>Register</NavLink>
          </div>
        )}
      </Toolbar>
    </AppBar>
  );
}
