import {
  Avatar,
  Button,
  Grid2,
  Input,
  MenuItem,
  Select,
  TextField,
  Typography,
} from '@mui/material';
import { useLocation, useNavigate } from 'react-router-dom';
import { EditSocialEventRequest, SocialEventModel } from '../../types/types';
import { useForm } from 'react-hook-form';
import { EditEvent } from '../../services/socialEvents';
import axios from 'axios';

type Props = {};
const EditSocialEventPage = (props: Props) => {
  const navigate = useNavigate();
  const location = useLocation();
  let { state } = location;
  state = state as EditSocialEventRequest;

  const { register, handleSubmit, watch } = useForm<EditSocialEventRequest>({
    defaultValues: {
      id: state.id,
      nameOfEvent: state.nameOfEvent,
      description: state.description,
      place: state.place,
      date: state.date,
      category: state.category,
      maxAttendee: state.maxAttendee,
    },
  });

  function onSubmit(data: EditSocialEventRequest) {
    const target: EditSocialEventRequest = {
      id: state.id,
      nameOfEvent: data.nameOfEvent,
      description: data.description,
      place: data.place,
      date: data.date,
      category: data.category,
      maxAttendee: data.maxAttendee,
    };

    EditEvent(data)
      .then(() => {
        navigate('/socialEvents');
      })
      .catch();
  }

  const handleFileUpload = async (event) => {
    event.preventDefault();
    const imageInput = document.getElementById('image');
    const formData = new FormData();
    const file = imageInput.files[0];
    if (!file) {
      return;
    }
    formData.append('formFile', file);

    await axios
      .put(
        `https://localhost:7127/SocialEvents/upload?id=${state.id}`,
        formData,
        {
          withCredentials: true,
        }
      )
      .then((response) => {
        console.log(response);
      })
      .catch((error) => {
        console.error(error);
      });
  };

  const selectedCategory = watch('category');
  const selectedDate = watch('date');
  return (
    <section>
      <Typography variant='h1'>Edit Event</Typography>
      <form onSubmit={handleSubmit(onSubmit)}>
        <Grid2
          container
          direction={'column'}
          gap={'20px'}
          alignItems={'center'}>
          <Grid2 size={5}>
            <TextField
              {...register('nameOfEvent')}
              fullWidth
              name='nameOfEvent'
              id='nameOfEvent'
              type='text'
              label='Name'
              required
            />
          </Grid2>
          <Grid2 size={5}>
            <TextField
              {...register('description')}
              fullWidth
              name='description'
              id='description'
              type='text'
              label='Description'
              required
            />
          </Grid2>
          <Grid2 size={5}>
            <TextField
              {...register('place')}
              fullWidth
              name='place'
              id='place'
              type='text'
              label='place'
              required
            />
          </Grid2>
          <Grid2 size={5}>
            <Select
              {...register('category')}
              fullWidth
              name='category'
              id='category'
              label='Category'
              value={selectedCategory || ''}
              required>
              <MenuItem key={'Other'} value={'Other'}>
                Other
              </MenuItem>
              <MenuItem key={'Conference'} value={'Conference'}>
                Conference
              </MenuItem>
              <MenuItem key={'Convention'} value={'Convention'}>
                Convention
              </MenuItem>
              <MenuItem key={'Lecture'} value={'Lecture'}>
                Lecture
              </MenuItem>
              <MenuItem key={'MasterClass'} value={'MasterClass'}>
                MasterClass
              </MenuItem>
              <MenuItem key={'QnA'} value={'QnA'}>
                Q&A
              </MenuItem>
            </Select>
          </Grid2>
          <Grid2 size={5}>
            <TextField
              {...register('date')}
              fullWidth
              name='date'
              id='date'
              type='date'
              label='Date'
              value={new Date(selectedDate).toISOString().split('T')[0] || ''}
              required
            />
          </Grid2>
          <Grid2 size={5}>
            <TextField
              {...register('maxAttendee')}
              fullWidth
              name='maxAttendee'
              id='maxAttendee'
              type='number'
              label='Max Attendees'
              required
            />
          </Grid2>

          <Grid2 size={5}>
            <Button type='submit' variant='contained'>
              Edit
            </Button>
          </Grid2>
        </Grid2>
      </form>
      <form onSubmit={handleFileUpload}>
        <div>
          <input accept='image/*' id='image' type='file' />
          <Button type='submit' variant='contained'>
            Upload
          </Button>
        </div>
      </form>
    </section>
  );
};

export default EditSocialEventPage;

// const ProfilePicture = ({ edit }) => {
//   const hiddenInputRef = useRef();

//   const [preview, setPreview] = useState<string>();

//   const { ref: registerRef, ...rest } = edit('profilePicture');

//   const handleUploadedFile = (event) => {
//     const file = event.target.files[0];

//     const urlImage = URL.createObjectURL(file);

//     setPreview(urlImage);
//   };

//   const onUpload = () => {
//     hiddenInputRef.current.click();
//   };

//   const uploadButtonLabel = preview ? 'Change image' : 'Upload image';

//   return (
//     <div>
//       <Label>Profile picture</Label>

//       <Input
//         type='file'
//         name='profilePicture'
//         {...rest}
//         onChange={handleUploadedFile}
//         ref={(e) => {
//           registerRef(e);
//           hiddenInputRef.current = e;
//         }}
//       />

//       <Avatar src={preview} sx={{ width: 80, height: 80 }} />

//       <Button variant='text' onClick={onUpload}>
//         {uploadButtonLabel}
//       </Button>
//     </div>
//   );
// };
