import { Navigate, useNavigate, useParams } from 'react-router-dom';
import {
  DeleteEvent,
  GetSocialEventByIdWithToken,
} from '../../services/socialEvents';
import { Role } from '../../types/types';
import {
  Box,
  Button,
  Chip,
  Container,
  Grid2,
  Paper,
  Typography,
} from '@mui/material';
import { useAuth } from '../../hooks/useAuth';
import DateDisplay from '../../components/DateDisplay';
import Loader from '../../components/Loader';
import { useFetch, useFetchAction } from '../../hooks/useFetch';

const EventPage = () => {
  const { role } = useAuth();
  const { id } = useParams();
  const navigate = useNavigate();
  const [event, eventLoading] = useFetch(async () =>
    id ? await GetSocialEventByIdWithToken(id) : null
  );
  const [deleteEvent] = useFetchAction(DeleteEvent);

  const handleDelete = async () => {
    if (!event) {
      return;
    }
    const result = await deleteEvent(event.id);
    if (result) {
      navigate('/socialEvents');
    }
  };

  if (!id) {
    return <Navigate to='/socialEvents' />;
  }

  if (eventLoading || !event) {
    return <Loader fullPage />;
  }

  const allowedToParticipate =
    event?.listOfAttendees.length < event?.maxAttendee &&
    !event?.isAlreadyInList;
  const attendeesNumber = event?.listOfAttendees?.length ?? 0;

  return (
    <Container maxWidth='xl' component={'section'} sx={{ mt: 20 }}>
      <Grid2 container gap={2} justifyContent={'center'}>
        <Grid2 container direction={'column'}>
          {event.image ? (
            <Box
              component='img'
              src={`${import.meta.env.VITE_IMAGES_HOST}/${event.image}`}
              alt={`${event.eventName} - cover`}
              title={`${event.eventName} - cover`}
              sx={{ width: 300, height: 300, mb: 2, objectFit: 'cover' }}
            />
          ) : null}
          <Grid2 container flexDirection={'column'} gap={1}>
            {!allowedToParticipate ? (
              <Button variant='contained' disabled>
                {event.isAlreadyInList ? 'Already Signed Up' : 'No places left'}
              </Button>
            ) : (
              <Button href={`/attendeePage/${event.id}`} variant='contained'>
                {attendeesNumber === 0
                  ? 'Be the First to Sign Up'
                  : 'Sign Up For Event'}
              </Button>
            )}
            <Typography variant='body2' textAlign={'center'}>
              {event.maxAttendee} places
            </Typography>
            {attendeesNumber !== 0 && (
              <Typography variant='body2' textAlign={'center'}>
                {attendeesNumber} people signed
              </Typography>
            )}
            {role === Role.Admin && (
              <Grid2
                container
                border={2.5}
                borderColor={(theme) => theme.palette.primary.light}
                borderRadius={2}
                padding={2}
                gap={2}
                mt={2}>
                <Button variant='contained' href={`/editEvent/${id}`} fullWidth>
                  Edit
                </Button>
                <Button
                  variant='contained'
                  color='error'
                  onClick={handleDelete}
                  fullWidth>
                  Delete
                </Button>
              </Grid2>
            )}
          </Grid2>
        </Grid2>
        <Grid2
          container
          flexDirection={'column'}
          sx={{
            width: 800,
          }}
          gap={2}
          justifyContent={'flex-start'}>
          <Paper sx={{ p: 2 }}>
            <Grid2
              container
              direction={'column'}
              alignItems={'flex-start'}
              gap={1}>
              <Typography variant='h4' component='h1'>
                {event.eventName}
              </Typography>
              <Chip label={event.category} />
              <Grid2 container direction={'column'} sx={{ mt: 4 }}>
                <Grid2 container gap={2}>
                  <Typography fontWeight={'bold'}>Date:</Typography>
                  <DateDisplay date={new Date(event.date)} />
                </Grid2>
                <Grid2 container gap={2}>
                  <Typography fontWeight={'bold'}>Location:</Typography>
                  <Typography>{event.place}</Typography>
                </Grid2>
              </Grid2>
            </Grid2>
          </Paper>
          <Paper
            sx={(theme) => ({
              p: 2,
              background: theme.palette.grey[100],
            })}>
            <Typography variant='body1'>{event.description}</Typography>
          </Paper>
        </Grid2>
      </Grid2>
    </Container>
  );
};

export default EventPage;
