import {
  Button,
  Grid2,
  MenuItem,
  Select,
  TextField,
  Typography,
} from '@mui/material';
import { Repository } from '../../utils/Repository';
import { useLocation, useNavigate } from 'react-router-dom';

type Props = {};
const EditSocialEventPage = (props: Props) => {
  const navigate = useNavigate();
  const location = useLocation();
  const { state } = location;
  function handleSubmit(event: React.FormEvent<HTMLFormElement>) {
    event.preventDefault();
    const target = {
      id: state.id,
      eventName: event.currentTarget.eventName.value,
      description: event.currentTarget.description.value,
      place: event.currentTarget.place.value,
      category: event.currentTarget.category.value,
      date: event.currentTarget.date.value,
      maxAttendee: event.currentTarget.maxAttendee.value,
      //image: event.currentTarget.image.value,
    };
    Repository.EditEvent(target)
      .then(() => {
        navigate('/socialEvents');
      })
      .catch();
  }

  return (
    <section>
      <Typography variant='h1'>Edit Event</Typography>
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
              value={state.eventName}
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
              value={state.description}
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
              value={state.place}
              required
            />
          </Grid2>
          <Grid2 size={5}>
            <Select
              fullWidth
              name='category'
              id='category'
              label='Category'
              value={state.category}
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
              value={state.date}
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
              value={state.maxAttendee}
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
              Edit
            </Button>
          </Grid2>
        </Grid2>
      </form>
    </section>
  );
};

export default EditSocialEventPage;
