import { Button, Container, Grid2, TextField, Typography } from '@mui/material';
import { useNavigate, useParams } from 'react-router-dom';
import { CreateAttendeeRequest } from '../../types/types';
import { Controller, useForm } from 'react-hook-form';
import { AddAttendeeToEvent } from '../../services/attendee';
import { DatePicker } from '@mui/x-date-pickers';
import dayjs from 'dayjs';
import { useFetchAction } from '../../hooks/useFetch';

export const CreateAttendeePage = () => {
  const navigate = useNavigate();
  const { id } = useParams();
  const { register, control, handleSubmit } = useForm<CreateAttendeeRequest>();
  const [addAttendee] = useFetchAction(AddAttendeeToEvent);

  const onSubmitAttendee = (data: CreateAttendeeRequest) => {
    if (!id) {
      return;
    }
    addAttendee(data, id).then(() => {
      navigate('/admissions');
    });
  };

  return (
    <Container maxWidth='xl' component='section' sx={{ mt: 20 }}>
      <Grid2 container direction={'column'} alignItems={'center'} gap={5}>
        <Typography variant='h4' component='h1'>
          Sign Up For Event
        </Typography>
        <form onSubmit={handleSubmit(onSubmitAttendee)}>
          <Grid2
            container
            direction={'column'}
            gap={'20px'}
            alignItems={'center'}
            width={700}>
            <TextField
              {...register('email')}
              fullWidth
              name='email'
              id='email'
              type='email'
              label='Contact Email'
              required
            />
            <TextField
              {...register('name')}
              fullWidth
              name='name'
              id='name'
              type='text'
              label='Name'
              required
            />
            <TextField
              {...register('surname')}
              fullWidth
              name='surname'
              id='surname'
              type='text'
              label='Surname'
              required
            />
            <Controller
              control={control}
              name='dateOfBirth'
              rules={{ required: true }}
              render={({ field: { value, ...props } }) => (
                <DatePicker
                  label='Date of Birth *'
                  value={value ? dayjs(value) : null}
                  {...props}
                  sx={{ width: '100%' }}
                />
              )}
            />

            <Button type='submit' variant='contained' fullWidth sx={{ mt: 2 }}>
              Sign Up
            </Button>
          </Grid2>
        </form>
      </Grid2>
    </Container>
  );
};
