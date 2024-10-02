import {
  Button,
  Grid2,
  MenuItem,
  Select,
  TextField,
  Typography,
} from '@mui/material';
import { useNavigate } from 'react-router-dom';
import { Repository } from '../../utils/Repository';

type Props = {};
const CreateSocialEventPage = (props: Props) => {
  const navigate = useNavigate();

  function handleSubmit(event: React.FormEvent<HTMLFormElement>) {
    event.preventDefault();
    const target = {
      eventName: event.currentTarget.eventName.value,
      description: event.currentTarget.description.value,
      place: event.currentTarget.place.value,
      category: event.currentTarget.category.value,
      date: event.currentTarget.date.value,
      maxAttendee: event.currentTarget.maxAttendee.value,
      //image: event.currentTarget.image.value,
    };
    Repository.CreateEvent(target)
      .then(() => {
        navigate('/socialEvents');
      })
      .catch();
  }

  return (
    <section>
      <Typography variant='h1'>Create Event</Typography>
      <form onSubmit={handleSubmit}>
        <Grid2
          container
          direction={'column'}
          gap={'20px'}
          alignItems={'center'}>
          <Grid2 size={5}>
            <TextField
              fullWidth
              name='name'
              id='eventName'
              type='text'
              label='Name'
              required
            />
          </Grid2>
          <Grid2 size={5}>
            <TextField
              fullWidth
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
              name='place'
              id='place'
              type='text'
              label='place'
              required
            />
          </Grid2>
          <Grid2 size={5}>
            <Select
              fullWidth
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
              name='date'
              id='date'
              type='date'
              label='date'
              required
            />
          </Grid2>
          <Grid2 size={5}>
            <TextField
              fullWidth
              name='maxAttendee'
              id='maxAttendee'
              type='number'
              label='Max Attendees'
              required
            />
          </Grid2>
          {/* <Grid2 size={5}>
            <TextField
              fullWidth
              name='Image'
              id='Image'
              type='image'
              label='Image'
            />
          </Grid2> */}
          <Grid2 size={5}>
            <Button type='submit' variant='contained'>
              Create
            </Button>
          </Grid2>
        </Grid2>
      </form>
    </section>
  );
};

export default CreateSocialEventPage;
