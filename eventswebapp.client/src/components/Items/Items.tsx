import { NavLink } from 'react-router-dom';
import { SocialEventModel } from '../../types/types';

interface Props {
  currentItems: Array<SocialEventModel>;
}
export default function Items(props: Props) {
  return (
    <>
      {props.currentItems &&
        props.currentItems.map((item) => (
          <NavLink key={item.id} to='/eventPage' state={item.id}>
            <p>{item.eventName}</p>
            <i>{item.category}</i>
            <i>{item.date.toString()}</i>
          </NavLink>
        ))}
    </>
  );
}
