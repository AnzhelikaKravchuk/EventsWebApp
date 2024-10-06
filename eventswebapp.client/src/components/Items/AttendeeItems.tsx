import { NavLink } from 'react-router-dom';
import { AttendeeModel } from '../../types/types';

interface Props {
  currentItems: Array<AttendeeModel>;
}
export default function AttendeeItems(props: Props) {
  return (
    <>
      {props.currentItems &&
        props.currentItems.map((item) => (
          <div>
            <p>{item.name}</p>
            <i>{item.surname}</i>
            <i>{item.dateOfBirth.toString()}</i>
          </div>
        ))}
    </>
  );
}
