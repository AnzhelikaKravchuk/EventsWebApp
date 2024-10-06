import {
  Avatar,
  Button,
  Grid2,
  Input,
  MenuItem,
  Select,
  TextField,
  Typography,
} from '@mui/material';
import { useLocation, useNavigate } from 'react-router-dom';
import { EditSocialEventRequest } from '../../types/types';
import { useForm } from 'react-hook-form';
import { EditEvent, UploadImage } from '../../services/socialEvents';

const EditSocialEventPage = () => {
  const navigate = useNavigate();
  const location = useLocation();
  let { state } = location;
  state = state as EditSocialEventRequest;

  const { register, handleSubmit, watch } = useForm<EditSocialEventRequest>({
    defaultValues: {
      id: state.id,
      eventName: state.eventName,
      description: state.description,
      place: state.place,
      date: state.date,
      category: state.category,
      maxAttendee: state.maxAttendee,
    },
  });

  const onSubmitEdit = (data: EditSocialEventRequest) => {
    EditEvent(data)
      .then(() => {
        navigate('/socialEvents');
      })
      .catch((err) => console.log(err));
  };

  const handleFileUpload = async (event) => {
    event.preventDefault();
    const imageInput = document.getElementById('image');
    const formData = new FormData();
    const file = imageInput.files[0];
    formData.append('formFile', file);

    UploadImage(formData, state.id);
  };

  const selectedCategory = watch('category');
  const selectedDate = watch('date');

  return (
    <section>
      <Typography variant='h1'>Edit Event</Typography>
      <form onSubmit={handleSubmit(onSubmitEdit)}>
        <Grid2
          container
          direction={'column'}
          gap={'20px'}
          alignItems={'center'}>
          <Grid2 size={5}>
            <TextField
              {...register('eventName')}
              fullWidth
              name='eventName'
              id='eventName'
              type='text'
              label='Name'
              required
            />
          </Grid2>
          <Grid2 size={5}>
            <TextField
              {...register('description')}
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
              {...register('place')}
              fullWidth
              name='place'
              id='place'
              type='text'
              label='Place'
              required
            />
          </Grid2>
          <Grid2 size={5}>
            <Select
              {...register('category')}
              fullWidth
              name='category'
              id='category'
              label='Category'
              value={selectedCategory || ''}
              required>
              <MenuItem key={'Other'} value={'Other'}>
                Other
              </MenuItem>
              <MenuItem key={'Conference'} value={'Conference'}>
                Conference
              </MenuItem>
              <MenuItem key={'Convention'} value={'Convention'}>
                Convention
              </MenuItem>
              <MenuItem key={'Lecture'} value={'Lecture'}>
                Lecture
              </MenuItem>
              <MenuItem key={'MasterClass'} value={'MasterClass'}>
                MasterClass
              </MenuItem>
              <MenuItem key={'QnA'} value={'QnA'}>
                Q&A
              </MenuItem>
            </Select>
          </Grid2>
          <Grid2 size={5}>
            <TextField
              {...register('date')}
              fullWidth
              name='date'
              id='date'
              type='date'
              label='Date'
              value={new Date(selectedDate).toISOString().split('T')[0] || ''}
              required
            />
          </Grid2>
          <Grid2 size={5}>
            <TextField
              {...register('maxAttendee')}
              fullWidth
              name='maxAttendee'
              id='maxAttendee'
              type='number'
              label='Max Attendees'
              required
            />
          </Grid2>

          <Grid2 size={5}>
            <Button type='submit' variant='contained'>
              Edit
            </Button>
          </Grid2>
        </Grid2>
      </form>
      <form onSubmit={handleFileUpload}>
        <div>
          <input accept='image/*' id='image' type='file' />
          <Button type='submit' variant='contained'>
            Upload
          </Button>
        </div>
      </form>
    </section>
  );
};

export default EditSocialEventPage;
