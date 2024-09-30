import { Button, Grid2, TextField, Typography } from '@mui/material';
import { useState } from 'react';
import { Repository } from '../../utils/Repository';
import { useAuth } from '../../hooks/useAuth';
import { useNavigate } from 'react-router-dom';
interface Props {}

const RegisterPage = (props: Props) => {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [username, setUsername] = useState('');
  const { login } = useAuth();
  const navigate = useNavigate();

  function handleSubmit() {
    Repository.Register({ email, password, username })
      .then(() => {
        login();
        navigate('/socialEvents');
      })
      .catch();
  }

  return (
    <section>
      <Typography variant='h1'>Register</Typography>
      <form onSubmit={handleSubmit}>
        <Grid2
          container
          direction={'column'}
          gap={'20px'}
          alignItems={'center'}>
          <Grid2 size={5}>
            <TextField
              fullWidth
              name='email'
              id='email'
              type='email'
              label='Email'
              onChange={(e) => setEmail(e.target.value)}
              required
            />
          </Grid2>
          <Grid2 size={5}>
            <TextField
              fullWidth
              name='password'
              id='password'
              type='password'
              label='Password'
              onChange={(e) => setPassword(e.target.value)}
              required
            />
          </Grid2>
          <Grid2 size={5}>
            <TextField
              fullWidth
              name='username'
              id='username'
              type='text'
              label='Username'
              onChange={(e) => setUsername(e.target.value)}
              required
            />
          </Grid2>
          <Grid2 size={5}>
            <Button type='submit' variant='contained'>
              Register
            </Button>
          </Grid2>
        </Grid2>
      </form>
    </section>
  );
};

export default RegisterPage;
