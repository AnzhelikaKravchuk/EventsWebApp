import { Button, Grid2, TextField, Typography } from '@mui/material';
import { useNavigate } from 'react-router-dom';
import { useForm } from 'react-hook-form';
import { RegisterRequest } from '../../types/types';
import { Register } from '../../services/user';
import { useAuth } from '../../hooks/useAuth';

const RegisterPage = () => {
  const { authenticate } = useAuth();
  const { register, handleSubmit } = useForm<RegisterRequest>();
  const navigate = useNavigate();

  async function onSubmit(data: RegisterRequest) {
    Register(data)
      .then(async () => {
        await authenticate();
        navigate('/socialEvents');
      })
      .catch();
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
          Create New Account
        </Typography>
      </Grid2>
      <Grid2 size={3}>
        <form onSubmit={handleSubmit(onSubmit)}>
          <Grid2 container direction={'column'} gap={'20px'}>
            <TextField
              fullWidth
              {...register('email')}
              name='email'
              id='email'
              type='email'
              label='Email'
              required
            />
            <TextField
              fullWidth
              {...register('password')}
              name='password'
              id='password'
              type='password'
              label='Password'
              required
            />
            <TextField
              fullWidth
              {...register('username')}
              name='username'
              id='username'
              type='text'
              label='Username'
              required
            />
            <Button type='submit' variant='contained'>
              Sign Up
            </Button>
          </Grid2>
        </form>
      </Grid2>
    </Grid2>
  );
};

export default RegisterPage;
