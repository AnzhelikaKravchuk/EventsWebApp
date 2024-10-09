import {
  Button,
  Container,
  Grid2,
  InputLabel,
  MenuItem,
  TextField,
  Typography,
} from '@mui/material';
import { useNavigate, useParams } from 'react-router-dom';
import { SocialEventRequest } from '../../types/types';
import { Controller, useForm } from 'react-hook-form';
import {
  CreateEvent,
  EditEvent,
  GetSocialEventById,
} from '../../services/socialEvents';
import { useFetchAction, useLazyFetch } from '../../hooks/useFetch';
import { useEffect, useState } from 'react';
import Loader from '../../components/Loader';
import { DateTimePicker } from '@mui/x-date-pickers';
import dayjs from 'dayjs';
import { ImageSharp } from '@mui/icons-material';

type Props = {
  isEdit?: boolean;
};

const EditSocialEventPage = ({ isEdit }: Props) => {
  const navigate = useNavigate();
  const { id } = useParams();
  const [requestEvents, socialEvent, socialEventLoading] = useLazyFetch(
    async (id: string) => GetSocialEventById(id)
  );
  const [createEvent] = useFetchAction(CreateEvent);
  const [editEvent] = useFetchAction(EditEvent);

  useEffect(() => {
    if (id && isEdit) {
      requestEvents(id);
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [id, isEdit]);

  const [image, setImage] = useState<Blob | null>(null);

  const { register, handleSubmit, control, setValue } =
    useForm<SocialEventRequest>({
      defaultValues: {},
    });

  useEffect(() => {
    if (!socialEvent) {
      return;
    }
    const { eventName, description, place, date, category, maxAttendee } =
      socialEvent;
    setValue('category', category);
    setValue('eventName', eventName);
    setValue('description', description);
    setValue('place', place);
    setValue('date', date);

    setValue('maxAttendee', maxAttendee);
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [socialEvent]);

  const onSubmitEdit = (data: SocialEventRequest) => {
    if (!id || !socialEvent) return;

    const formData = new FormData();
    formData.append('id', socialEvent.id);
    formData.append('eventName', data.eventName);
    formData.append('description', data.description);
    formData.append('category', data.category);
    formData.append('date', data.date.toString());
    formData.append('maxAttendee', data.maxAttendee.toString());
    formData.append('place', data.place);
    if (socialEvent.image) {
      formData.append('image', socialEvent.image);
    }
    if (image) {
      formData.append('file', image);
    }
    editEvent(formData).then(() => {
      navigate('/socialEvents');
    });
  };

  const onSubmitCreate = (data: SocialEventRequest) => {
    const formData = new FormData();
    formData.append('eventName', data.eventName);
    formData.append('description', data.description);
    formData.append('category', data.category);
    formData.append('date', data.date.toString());
    formData.append('maxAttendee', data.maxAttendee.toString());
    formData.append('place', data.place);
    if (image) {
      formData.append('file', image);
    }

    createEvent(formData).then((res) => {
      navigate(`/eventPage/${res}`);
    });
  };

  if (socialEventLoading) {
    return <Loader fullPage />;
  }

  return (
    <Container maxWidth='xl' component={'section'} sx={{ mt: 20 }}>
      <Grid2 container direction={'column'} alignItems={'center'}>
        <Typography variant='h4' component='h1' mb={5} width={800}>
          {isEdit ? 'Edit Event' : 'Create New Event'}
        </Typography>
        <Grid2 container gap={2}>
          <Grid2 container direction={'column'} width={300} gap={2}>
            <InputLabel
              htmlFor='image'
              sx={(theme) => ({
                border: 2,
                borderRadius: 2,
                borderColor: theme.palette.primary.light,
                display: 'flex',
                flexDirection: 'column',
                justifyContent: 'center',
                alignItems: 'center',
                cursor: 'pointer',
                width: 300,
                height: 300,
                background: theme.palette.grey[200],
              })}>
              {image || socialEvent?.image ? (
                <img
                  src={
                    image
                      ? URL.createObjectURL(image)
                      : `${import.meta.env.VITE_IMAGES_HOST}/${
                          socialEvent?.image
                        }`
                  }
                  style={{
                    width: '100%',
                    height: '100%',
                    objectFit: 'cover',
                  }}
                />
              ) : (
                <>
                  <ImageSharp sx={{ width: 100, height: 100 }} />
                  <Typography>Add Image</Typography>
                </>
              )}
            </InputLabel>
            <input
              accept='image/*'
              id='image'
              type='file'
              style={{ display: 'none' }}
              onChange={(event) => {
                if (!event.target.files) {
                  setImage(null);
                  return;
                }
                setImage(event.target.files[0]);
              }}
            />
          </Grid2>
          <form
            onSubmit={handleSubmit((data) =>
              isEdit ? onSubmitEdit(data) : onSubmitCreate(data)
            )}>
            <Grid2
              container
              direction={'column'}
              gap={'20px'}
              width={500}
              alignItems={'center'}>
              <TextField
                {...register('eventName')}
                fullWidth
                name='eventName'
                id='eventName'
                type='text'
                label='Name'
                required
              />
              <TextField
                {...register('description')}
                fullWidth
                name='description'
                id='description'
                type='text'
                label='Description'
                required
                multiline
                rows={6}
              />
              <TextField
                {...register('place')}
                fullWidth
                name='place'
                id='place'
                type='text'
                label='Place'
                required
              />
              <Controller
                control={control}
                name='category'
                defaultValue=''
                rules={{ required: true }}
                render={({ field }) => {
                  return (
                    <TextField
                      select
                      {...field}
                      fullWidth
                      label='Category'
                      required>
                      <MenuItem value={''} hidden disabled></MenuItem>
                      <MenuItem value={'Other'}>Other</MenuItem>
                      <MenuItem value={'Conference'}>Conference</MenuItem>
                      <MenuItem value={'Convention'}>Convention</MenuItem>
                      <MenuItem value={'Lecture'}>Lecture</MenuItem>
                      <MenuItem value={'MasterClass'}>MasterClass</MenuItem>
                      <MenuItem value={'QnA'}>Q&A</MenuItem>
                    </TextField>
                  );
                }}
              />
              <Controller
                control={control}
                name='date'
                rules={{ required: true }}
                render={({ field: { value, ...props } }) => (
                  <DateTimePicker
                    label='Date *'
                    value={value ? dayjs(value) : null}
                    {...props}
                    sx={{ width: '100%' }}
                  />
                )}
              />
              <TextField
                {...register('maxAttendee')}
                fullWidth
                name='maxAttendee'
                id='maxAttendee'
                type='number'
                label='Max Attendees'
                required
              />

              <Button type='submit' fullWidth variant='contained'>
                {isEdit ? 'Update' : 'Create'}
              </Button>
            </Grid2>
          </form>
        </Grid2>
      </Grid2>
    </Container>
  );
};

export default EditSocialEventPage;
