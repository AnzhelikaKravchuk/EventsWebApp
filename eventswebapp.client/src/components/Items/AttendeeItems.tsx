import { useNavigate } from 'react-router-dom';
import { AttendeeModel } from '../../types/types';
import { DeleteAttendee } from '../../services/attendee';
import { Button, Card, CardContent, Grid2, Typography } from '@mui/material';
import DateDisplay from '../DateDisplay';
import { useFetchAction } from '../../hooks/useFetch';

interface Props {
  currentItems: Array<AttendeeModel>;
}
export default function AttendeeItems(props: Props) {
  const navigate = useNavigate();
  const [deleteAttendee] = useFetchAction(DeleteAttendee);

  const handleDelete = async (id: string) => {
    await deleteAttendee(id).then(() => {
      navigate(0);
    });
  };
  return (
    <Grid2 container gap={2} flexDirection={'row'} wrap='wrap' sx={{ mt: 20 }}>
      {props.currentItems &&
        props.currentItems.map((item) => (
          <Card key={item.id} component='article' sx={{ width: 550 }}>
            <CardContent sx={{ p: 3 }}>
              <Typography variant='h6' sx={{ fontWeight: 'bold', mb: 1 }}>
                {item.socialEventName}
              </Typography>
              <Grid2 container gap={1}>
                <Typography fontWeight={'bold'}>Full Name:</Typography>{' '}
                <Typography>{item.name + ' ' + item.surname}</Typography>
              </Grid2>
              <Grid2 container gap={1}>
                <Typography fontWeight={'bold'}>Email:</Typography>{' '}
                <Typography>{item.email}</Typography>
              </Grid2>
              <Grid2 container gap={1}>
                <Typography fontWeight={'bold'}>Date of Birth:</Typography>
                <DateDisplay date={new Date(item.dateOfBirth)} hideTime />
              </Grid2>
              <Grid2 container gap={1}>
                <Typography fontWeight={'bold'}>Signed Up:</Typography>
                <DateDisplay
                  date={new Date(item.dateOfRegistration)}
                  hideTime
                />
              </Grid2>
              <Button
                variant='contained'
                color='error'
                onClick={() => handleDelete(item.id)}
                sx={{ mt: 2 }}>
                Delete
              </Button>
            </CardContent>
          </Card>
        ))}
    </Grid2>
  );
}
