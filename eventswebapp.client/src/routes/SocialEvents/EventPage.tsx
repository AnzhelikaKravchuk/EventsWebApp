import { NavLink } from 'react-router-dom';
import { useLocation } from 'react-router-dom';

const EventPage = (props: Props) => {
  const location = useLocation();
  const { state } = location;
  return (
    <div>
      <NavLink to='/editEvent' state={state}>
        Edit Page
      </NavLink>
      <h2>{state.eventName}</h2>
      <p>{state.description}</p>
      <p>{state.date.toUTCString()}</p>
      <p>{state.place}</p>
      <p>{state.category}</p>
      <p>{state.maxAttendee}</p>
      <NavLink to='/socialEvents'>Back</NavLink>
    </div>
  );
};

export default EventPage;
