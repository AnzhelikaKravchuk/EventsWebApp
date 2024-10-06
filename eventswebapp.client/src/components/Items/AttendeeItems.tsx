import { NavLink, useNavigate } from 'react-router-dom';
import { AttendeeModel } from '../../types/types';
import { DeleteAttendee } from '../../services/attendee';
import { Button } from '@mui/material';

interface Props {
  currentItems: Array<AttendeeModel>;
}
export default function AttendeeItems(props: Props) {
  const navigate = useNavigate();
  const handleDelete = async (id: string) => {
    await DeleteAttendee(id).then(() => {
      navigate(0);
    });
  };
  return (
    <>
      {props.currentItems &&
        props.currentItems.map((item) => (
          <div>
            <p>{item.name}</p>
            <i>{item.surname}</i>
            <i>{item.dateOfBirth.toString()}</i>
            <Button variant='contained' onClick={() => handleDelete(item.id)}>
              Delete Application
            </Button>
          </div>
        ))}
    </>
  );
}
