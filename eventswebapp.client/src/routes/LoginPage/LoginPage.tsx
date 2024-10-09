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
    <Grid2
      container
      direction={'column'}
      alignItems='center'
      gap={'40px'}
      margin={20}>
      <Grid2 size={3}>
        <Typography variant='h4' component='h1' textAlign='center'>
          Login to Your Account
        </Typography>
      </Grid2>
      <Grid2 size={3}>
        <form onSubmit={handleSubmit(onSubmit)} style={{ width: '100%' }}>
          <Grid2 container direction={'column'} gap={'20px'}>
            <TextField
              {...register('email')}
              fullWidth
              name='email'
              id='email'
              type='email'
              label='Email'
              required
            />
            <TextField
              {...register('password')}
              fullWidth
              name='password'
              id='password'
              type='password'
              label='Password'
              required
            />
            <Button type='submit' variant='contained'>
              Login
            </Button>
          </Grid2>
        </form>
      </Grid2>
      <Typography variant='h6'>{errorMessage}</Typography>
    </Grid2>
  );
};

export default LoginPage;
