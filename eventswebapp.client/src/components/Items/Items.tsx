import { NavLink } from 'react-router-dom';
import { SocialEventModel } from '../../types/types';

interface Props {
  currentItems: Array<SocialEventModel>;
}
export default function Items(props: Props) {
  console.log(props.currentItems);
  return (
    <>
      {props.currentItems &&
        props.currentItems.map((item) => (
          <NavLink to='/eventPage' state={item}>
            <p>{item.nameOfEvent}</p>
          </NavLink>
        ))}
    </>
  );
}
