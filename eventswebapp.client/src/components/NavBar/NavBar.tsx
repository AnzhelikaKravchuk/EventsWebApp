import { AppBar, Toolbar, Typography } from '@mui/material';
import { NavLink } from 'react-router-dom';
interface Props {
  isAuthenticated: boolean;
}

export default function NavBar(props: Props) {
  return (
    <AppBar position='static'>
      <Toolbar variant='dense'>
        <Typography variant='h2'>Events Web Application</Typography>
        {props.isAuthenticated ? (
          <div>
            <NavLink to='/socialEvents'>Social Events</NavLink>
            <NavLink to='/admissions'>Your Admissions</NavLink>
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
