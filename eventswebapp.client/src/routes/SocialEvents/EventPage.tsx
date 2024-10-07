import { Navigate, NavLink, useNavigate, useParams } from 'react-router-dom';
import { useEffect, useState } from 'react';
import { DeleteEvent, GetSocialEventById } from '../../services/socialEvents';
import { Role, SocialEventModel } from '../../types/types';
import {
  Box,
  Button,
  Container,
  Grid2,
  IconButton,
  Paper,
} from '@mui/material';
import { useAuth } from '../../hooks/useAuth';
import Image from '../../components/Image';
import DeleteIcon from '@mui/icons-material/Delete';

const EventPage = () => {
  const { role } = useAuth();
  const { id } = useParams();
  const navigate = useNavigate();
  const [socialEvent, setSocialEvent] = useState<SocialEventModel | null>(null);
  const [eventLoading, setEventLoading] = useState(true);
  const [allowedToParticipate, setAllowedToParticipate] = useState(true);

  useEffect(() => {
    const fetchEvent = async () => {
      if (!id) {
        return;
      }
      return await GetSocialEventById(id)
        .then((res) => {
          setSocialEvent(res);
          setAllowedToParticipate(
            res.listOfAttendees.$values.length < res.maxAttendee
          );
        })
        .catch((err) => console.log(err))
        .finally(() => setEventLoading(false));
    };
    fetchEvent();

    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [eventLoading]);

  const handleDelete = async () => {
    if (!socialEvent) {
      return;
    }
    const result = await DeleteEvent(socialEvent.id).then();
    if (result) {
      navigate('/socialEvents');
    }
  };

  if (!id) {
    return <Navigate to='/socialEvents' />;
  }

  return (
    <div>
      {!eventLoading && socialEvent ? (
        <Container maxWidth='xl' component={'section'} sx={{ mt: 20 }}>
          <Grid2 container>
            <Grid2 container direction={'column'}>
              <Box
                component='img'
                src={import.meta.env.VITE_IMAGES_HOST + socialEvent.image}
                alt={`${socialEvent.eventName} - cover`}
                title={`${socialEvent.eventName} - cover`}
                sx={{ width: 300, height: 300, mb: 2 }}
              />
              <Grid2 container flexDirection={'column'} gap={1}>
                {!allowedToParticipate ? (
                  <h3>No places left</h3>
                ) : (
                  <Button href='/attendeePage' variant='contained'>
                    Sign Up For Event
                  </Button>
                )}
                <Button
                  sx={{ display: role !== Role.Admin ? 'none' : 'flex' }}
                  variant='contained'
                  color='error'
                  onClick={handleDelete}
                  startIcon={<DeleteIcon />}>
                  Delete Event
                </Button>
              </Grid2>
            </Grid2>
            <Grid2>
              <Paper>
                <h1>{socialEvent.eventName}</h1>
                <p>{socialEvent.description}</p>
                <p>{socialEvent.date.toString()}</p>
                <p>{socialEvent.place}</p>
                <p>{socialEvent.category}</p>
                <p>{socialEvent.maxAttendee}</p>
                <p>{socialEvent.listOfAttendees.$values.length}</p>
              </Paper>
            </Grid2>
          </Grid2>
        </Container>
      ) : (
        'Loading...'
      )}
      <NavLink hidden={role !== Role.Admin} to='/editEvent' state={socialEvent}>
        Edit Page
      </NavLink>

      <NavLink to='/socialEvents'>Back</NavLink>
    </div>
  );
};

export default EventPage;
