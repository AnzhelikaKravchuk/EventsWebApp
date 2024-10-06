import { Location, NavLink, useNavigate } from 'react-router-dom';
import { useLocation } from 'react-router-dom';
import { useEffect, useState } from 'react';
import { DeleteEvent, GetSocialEventById } from '../../services/socialEvents';
import { Role, SocialEventModel } from '../../types/types';
import { Button } from '@mui/material';
import { Delete } from '@mui/icons-material';
import { useAuth } from '../../hooks/useAuth';

const EventPage = (props: Props) => {
  const { role } = useAuth();
  const navigate = useNavigate();
  const location = useLocation();
  const { state } = location;
  const [socialEvent, setSocialEvent] = useState<SocialEventModel>({});
  const [eventLoading, setEventLoading] = useState(true);
  const [allowedToParticipate, setAllowedToParticipate] = useState(true);

  useEffect(() => {
    console.log(socialEvent);
    console.log();
    const fetchEvent = async () => {
      return await GetSocialEventById(state)
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
    const result = await DeleteEvent(socialEvent.id).then();
    if (result) {
      navigate('/socialEvents');
    }
  };

  return (
    <div>
      {!eventLoading ? (
        <section>
          <h2>{socialEvent?.eventName}</h2>
          <img
            src={'https://localhost:7127/' + socialEvent?.image}
            width={'400px'}
            height={'400px'}
          />
          <p>{socialEvent.description}</p>
          <p>{socialEvent.date.toString()}</p>
          <p>{socialEvent.place}</p>
          <p>{socialEvent.category}</p>
          <p>{socialEvent.maxAttendee}</p>
          <p>{socialEvent?.listOfAttendees.$values.length}</p>
          {!allowedToParticipate ? (
            <h3>No places left</h3>
          ) : (
            <NavLink to='/attendeePage' state={socialEvent?.id}>
              <Button variant='contained'>Participate in event</Button>
            </NavLink>
          )}
          <Button
            sx={{ display: role !== Role.Admin ? 'none' : 'inline-block' }}
            variant='contained'
            onClick={handleDelete}>
            Delete Event
          </Button>
        </section>
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
