import { Button, Grid2, Select, TextField } from '@mui/material';
import { useLocation, useNavigate } from 'react-router-dom';
import { CreateAttendeeRequest } from '../../types/types';
import { useForm } from 'react-hook-form';
import { AddAttendeeToEvent } from '../../services/attendee';

export const CreateAttendeePage = () => {
  const navigate = useNavigate();
  const location = useLocation();
  const { state } = location;
  const { register, handleSubmit } = useForm<CreateAttendeeRequest>();

  const onSubmitAttendee = (data: CreateAttendeeRequest) => {
    console.log(state);
    AddAttendeeToEvent(data, state)
      .then(() => {
        navigate('/admissions');
      })
      .catch((err) => console.log(err));
  };

  return (
    <div>
      <form onSubmit={handleSubmit(onSubmitAttendee)}>
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
              label='Contact Email'
              required
            />
          </Grid2>
          <Grid2 size={5}>
            <TextField
              {...register('name')}
              fullWidth
              name='name'
              id='name'
              type='text'
              label='Name'
              required
            />
          </Grid2>
          <Grid2 size={5}>
            <TextField
              {...register('surname')}
              fullWidth
              name='surname'
              id='surname'
              type='text'
              label='Surname'
              required
            />
          </Grid2>
          <Grid2 size={5}>
            <TextField
              {...register('dateOfBirth')}
              fullWidth
              name='dateOfBirth'
              id='dateOfBirth'
              type='date'
              label='Date Of Birth'
              required
            />

            <Grid2 size={5}>
              <Button type='submit' variant='contained'>
                Participate
              </Button>
            </Grid2>
          </Grid2>
        </Grid2>
      </form>
    </div>
  );
};
