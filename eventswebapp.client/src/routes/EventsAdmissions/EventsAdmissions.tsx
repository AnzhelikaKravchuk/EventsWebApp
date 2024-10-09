import { GetAttendeesByUser } from '../../services/attendee';
import AttendeeItems from '../../components/Items/AttendeeItems';
import { Container } from '@mui/material';
import { useFetch } from '../../hooks/useFetch';
import Loader from '../../components/Loader';

export default function EventsAdmissions() {
  const [attendees, attendeesLoading] = useFetch(() => GetAttendeesByUser());
  return (
    <Container maxWidth='xl'>
      {!attendeesLoading && attendees ? (
        <AttendeeItems currentItems={attendees} />
      ) : (
        <Loader fullPage />
      )}
    </Container>
  );
}
