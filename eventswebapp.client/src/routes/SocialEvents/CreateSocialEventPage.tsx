import {
  Button,
  Grid2,
  MenuItem,
  Select,
  TextField,
  Typography,
} from '@mui/material';
import { useNavigate } from 'react-router-dom';
import { useForm } from 'react-hook-form';
import { CreateSocialEventRequest } from '../../types/types';
import { CreateEvent } from '../../services/socialEvents';

const CreateSocialEventPage = () => {
  const navigate = useNavigate();
  const { register, handleSubmit } = useForm<CreateSocialEventRequest>();

  function onSubmitCreate(data: CreateSocialEventRequest) {
    const imageInput = document.getElementById('image');

    const formData = new FormData();
    const file = imageInput.files[0];
    formData.append('eventName', data.eventName);
    formData.append('description', data.description);
    formData.append('category', data.category);
    formData.append('date', data.date.toString());
    formData.append('maxAttendee', data.maxAttendee.toString());
    formData.append('place', data.place);
    formData.append('file', file);

    console.log(formData);
    CreateEvent(formData)
      .then((res) => {
        navigate('/eventPage', { state: { ...res } });
      })
      .catch();
  }

  return (
    <section>
      <Typography variant='h1'>Create Event</Typography>
      <form onSubmit={handleSubmit(onSubmitCreate)}>
        <Grid2
          container
          direction={'column'}
          gap={'20px'}
          alignItems={'center'}>
          <Grid2 size={5}>
            <TextField
              fullWidth
              {...register('eventName')}
              name='eventName'
              id='eventName'
              type='text'
              label='Name'
              required
            />
          </Grid2>
          <Grid2 size={5}>
            <TextField
              fullWidth
              {...register('description')}
              name='description'
              id='description'
              type='text'
              label='Description'
              required
            />
          </Grid2>
          <Grid2 size={5}>
            <TextField
              fullWidth
              {...register('place')}
              name='place'
              id='place'
              type='text'
              label='Place'
              required
            />
          </Grid2>
          <Grid2 size={5}>
            <Select
              fullWidth
              {...register('category')}
              name='category'
              id='category'
              label='Category'
              required>
              <MenuItem value={'Other'}>Other</MenuItem>
              <MenuItem value={'Conference'}>Conference</MenuItem>
              <MenuItem value={'Convention'}>Convention</MenuItem>
              <MenuItem value={'Lecture'}>Lecture</MenuItem>
              <MenuItem value={'MasterClass'}>MasterClass</MenuItem>
              <MenuItem value={'QnA'}>Q&A</MenuItem>
            </Select>
          </Grid2>
          <Grid2 size={5}>
            <TextField
              fullWidth
              {...register('date')}
              name='date'
              id='date'
              type='date'
              label='Date'
              required
            />
          </Grid2>
          <Grid2 size={5}>
            <TextField
              fullWidth
              {...register('maxAttendee')}
              name='maxAttendee'
              id='maxAttendee'
              type='number'
              label='Max Attendees'
              required
            />
          </Grid2>
          <Grid2 size={5}>
            <Button type='submit' variant='contained'>
              Create
            </Button>
          </Grid2>
        </Grid2>
        <Grid2 size={5}>
          <input accept='image/*' id='image' type='file' />
        </Grid2>
      </form>
    </section>
  );
};

export default CreateSocialEventPage;
