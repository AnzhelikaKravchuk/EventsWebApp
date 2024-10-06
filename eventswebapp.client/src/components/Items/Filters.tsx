import { NavLink, useNavigate } from 'react-router-dom';
import { AppliedFilters, SocialEventModel } from '../../types/types';
import { useForm } from 'react-hook-form';
import {
  Box,
  Button,
  FormControl,
  Grid2,
  InputLabel,
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
    <Grid2 container direction={'column'} sx={{ padding: 6 }}>
      <form onSubmit={handleSubmit(onSubmitFilters)} css={{ width: '100%' }}>
        <Grid2
          container
          direction={'column'}
          gap={'20px'}
          alignItems={'center'}>
          <TextField
            fullWidth
            {...register('name')}
            name='name'
            id='name'
            type='text'
            label='Name'
          />
          <TextField
            fullWidth
            {...register('date')}
            name='date'
            id='date'
            type='date'
            label='Date'
          />
          <TextField
            fullWidth
            {...register('place')}
            name='place'
            id='place'
            type='text'
            label='Place'
          />
          <FormControl fullWidth>
            <InputLabel>Category</InputLabel>{' '}
            <Select
              label='Category'
              fullWidth
              {...register('category')}
              name='category'
              id='category'>
              <MenuItem value={''}>
                <em>All</em>
              </MenuItem>
              <MenuItem value={'Other'}>Other</MenuItem>
              <MenuItem value={'Conference'}>Conference</MenuItem>
              <MenuItem value={'Convention'}>Convention</MenuItem>
              <MenuItem value={'Lecture'}>Lecture</MenuItem>
              <MenuItem value={'MasterClass'}>MasterClass</MenuItem>
              <MenuItem value={'QnA'}>Q&A</MenuItem>
            </Select>
          </FormControl>

          <Button fullWidth type='submit' variant='contained'>
            Apply Filters
          </Button>
        </Grid2>
      </form>
    </Grid2>
  );
}
