import { NavLink, useNavigate } from 'react-router-dom';
import { AppliedFilters, SocialEventModel } from '../../types/types';
import { useForm } from 'react-hook-form';
import {
  Button,
  Grid2,
  MenuItem,
  Select,
  TextField,
  Typography,
} from '@mui/material';
import { GetSocialEvents } from '../../services/socialEvents';

type Props = {
  pageSize: number;
  setItems: Function;
  setPages: Function;
  setFilters: Function;
  setPageIndex: Function;
};

export default function Filters(props: Props) {
  const navigate = useNavigate();
  const { register, handleSubmit } = useForm<AppliedFilters>();

  function onSubmitFilters(data: AppliedFilters) {
    const formData = new FormData();
    formData.append('name', data.name);
    formData.append('category', data.category);
    formData.append('date', data.date.toString());
    formData.append('place', data.place);

    console.log(formData);
    props.setFilters(formData);
    props.setPageIndex(1);

    // GetSocialEvents(formData, 1, props.pageSize)
    //   .then((res) => {
    //     console.log(res.totalPages);
    //     props.setItems(res.items.$values);
    //     props.setPages(res.totalPages);
    //   })
    //   .catch();
  }
  return (
    <div>
      <Typography variant='h3'>Apply Filters</Typography>
      <form onSubmit={handleSubmit(onSubmitFilters)}>
        <Grid2
          container
          direction={'column'}
          gap={'20px'}
          alignItems={'center'}>
          <Grid2 size={5}>
            <TextField
              fullWidth
              {...register('name')}
              name='name'
              id='name'
              type='text'
              label='Name'
            />
          </Grid2>
          <Grid2 size={5}>
            <TextField
              fullWidth
              {...register('date')}
              name='date'
              id='date'
              type='text'
              label='Date'
            />
          </Grid2>
          <Grid2 size={5}>
            <TextField
              fullWidth
              {...register('place')}
              name='place'
              id='place'
              type='text'
              label='Place'
            />
          </Grid2>
          <Grid2 size={5}>
            <Select
              fullWidth
              {...register('category')}
              name='category'
              id='category'
              label='Category'>
              <MenuItem value={'Other'}>Other</MenuItem>
              <MenuItem value={'Conference'}>Conference</MenuItem>
              <MenuItem value={'Convention'}>Convention</MenuItem>
              <MenuItem value={'Lecture'}>Lecture</MenuItem>
              <MenuItem value={'MasterClass'}>MasterClass</MenuItem>
              <MenuItem value={'QnA'}>Q&A</MenuItem>
              <MenuItem value={''}>None</MenuItem>
            </Select>
          </Grid2>

          <Grid2 size={5}>
            <Button type='submit' variant='contained'>
              Filter
            </Button>
          </Grid2>
        </Grid2>
      </form>
    </div>
  );
}
