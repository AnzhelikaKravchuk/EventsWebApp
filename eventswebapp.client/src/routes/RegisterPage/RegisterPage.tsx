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
    <section>
      <Typography variant='h1'>Register</Typography>
      <form onSubmit={handleSubmit(onSubmit)}>
        <Grid2
          container
          direction={'column'}
          gap={'20px'}
          alignItems={'center'}>
          <Grid2 size={5}>
            <TextField
              fullWidth
              {...register('email')}
              name='email'
              id='email'
              type='email'
              label='Email'
              required
            />
          </Grid2>
          <Grid2 size={5}>
            <TextField
              fullWidth
              {...register('password')}
              name='password'
              id='password'
              type='password'
              label='Password'
              required
            />
          </Grid2>
          <Grid2 size={5}>
            <TextField
              fullWidth
              {...register('username')}
              name='username'
              id='username'
              type='text'
              label='Username'
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
