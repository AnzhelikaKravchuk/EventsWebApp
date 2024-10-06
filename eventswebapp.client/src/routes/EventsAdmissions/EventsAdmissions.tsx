import { useEffect, useState } from 'react';
import { AttendeeModel } from '../../types/types';
import { GetAttendeesByUser } from '../../services/attendee';
import AttendeeItems from '../../components/Items/AttendeeItems';

export default function EventsAdmissions() {
  const [attendeeList, setAttendeeList] = useState<Array<AttendeeModel>>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const fetchAttendees = async () => {
      return await GetAttendeesByUser()
        .then((res) => {
          setAttendeeList(res);
          console.log(res);
        })
        .catch((err) => console.log(err))
        .finally(() => setLoading(false));
    };
    fetchAttendees();
  }, [loading]);

  return (
    <div>
      {!loading ? <AttendeeItems currentItems={attendeeList} /> : 'Loading...'}
    </div>
  );
}
