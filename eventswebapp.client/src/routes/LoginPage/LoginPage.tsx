import { Button, Grid2, TextField, Typography } from '@mui/material';
import { useNavigate } from 'react-router-dom';
import { Login } from '../../services/user';
import { LoginRequest } from '../../types/types';
import { useForm } from 'react-hook-form';
import { useState } from 'react';
import { useAuth } from '../../hooks/useAuth';
import { AxiosError } from 'axios';

const LoginPage = () => {
  const [errorMessage, setErrorMessage] = useState('');
  const { authenticate } = useAuth();
  const { register, handleSubmit } = useForm<LoginRequest>();
  const navigate = useNavigate();

  async function onSubmit(data: LoginRequest) {
    Login(data)
      .then(async () => {
        await authenticate();
        setErrorMessage('');
        navigate('/socialEvents');
      })
      .catch((error: AxiosError) => {
        setErrorMessage(error.message);
        console.log(errorMessage);
      });
  }

  return (
    <section>
      <Typography variant='h1'>Enter your credentials</Typography>
      <form onSubmit={handleSubmit(onSubmit)}>
        <Grid2
          container
          direction={'column'}
          gap={'20px'}
          alignItems={'center'}>
          <Grid2 size={5}>
            <TextField
              {...register('email')}
              fullWidth
              name='email'
              id='email'
              type='email'
              label='Email'
              required
            />
          </Grid2>
          <Grid2 size={5}>
            <TextField
              {...register('password')}
              fullWidth
              name='password'
              id='password'
              type='password'
              label='Password'
              required
            />
          </Grid2>
          <Grid2 size={5}>
            <Button type='submit' variant='contained'>
              Login
            </Button>
          </Grid2>
        </Grid2>
      </form>
      <Typography variant='h6'>{errorMessage}</Typography>
    </section>
  );
};

export default LoginPage;
