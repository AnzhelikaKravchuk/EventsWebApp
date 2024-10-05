import { Location, NavLink } from 'react-router-dom';
import { useLocation } from 'react-router-dom';
import { SocialEventModel } from '../../types/types';

const EventPage = (props: Props) => {
  const location: Location<SocialEventModel> = useLocation();
  const { state } = location;
  return (
    <div>
      <NavLink to='/editEvent' state={state}>
        Edit Page
      </NavLink>
      <h2>{state.nameOfEvent}</h2>
      <p>{state.description}</p>
      <p>{state.date.toString()}</p>
      <p>{state.place}</p>
      <p>{state.category}</p>
      <p>{state.maxAttendee}</p>
      <NavLink to='/socialEvents'>Back</NavLink>
    </div>
  );
};

export default EventPage;
