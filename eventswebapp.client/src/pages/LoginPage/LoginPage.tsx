import {
  Button,
  FormGroup,
  FormLabel,
  Grid2,
  TextField,
  Typography,
} from '@mui/material';
import { useState } from 'react';
import { Repository } from '../../utils/Repository';

type Props = {};

const LoginPage = (props: Props) => {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');

  function handleSubmit() {
    Repository.Login({ email, password });
  }

  return (
    <section>
      <Typography variant='h1'>Enter your credentials</Typography>
      <form>
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
            />
          </Grid2>
          <Grid2 size={5}>
            <Button onClick={handleSubmit} variant='contained'>
              Login
            </Button>
          </Grid2>
        </Grid2>
      </form>
    </section>
  );
};

export default LoginPage;
