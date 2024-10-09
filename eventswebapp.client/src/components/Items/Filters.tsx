import { AppliedFilters } from '../../types/types';
import { Controller, useForm } from 'react-hook-form';
import { Button, Grid2, MenuItem, TextField } from '@mui/material';
import { DatePicker } from '@mui/x-date-pickers';
import dayjs from 'dayjs';

type Props = {
  pageSize: number;
  setFilters: (filters: FormData) => void;
  setPageIndex: (pageIndex: number) => void;
};

export default function Filters(props: Props) {
  const { register, control, handleSubmit } = useForm<AppliedFilters>();

  function onSubmitFilters(data: AppliedFilters) {
    const formData = new FormData();
    formData.append('name', data.name);
    formData.append('category', data.category);
    formData.append('date', data.date?.toString() ?? '');
    formData.append('place', data.place);

    props.setFilters(formData);
    props.setPageIndex(1);
  }
  return (
    <Grid2 container direction={'column'} sx={{ padding: 6 }}>
      <form onSubmit={handleSubmit(onSubmitFilters)} style={{ width: '100%' }}>
        <Grid2 container direction={'column'} gap={'20px'}>
          <TextField
            fullWidth
            {...register('name')}
            name='name'
            id='name'
            type='text'
            label='Name'
          />
          <Controller
            control={control}
            name='date'
            render={({ field: { onChange, value, ref } }) => {
              return (
                <DatePicker
                  label='Date'
                  value={value ? dayjs(value) : null}
                  inputRef={ref}
                  onChange={(date) => {
                    onChange(date);
                  }}
                />
              );
            }}
          />
          <TextField
            fullWidth
            {...register('place')}
            name='place'
            id='place'
            type='text'
            label='Place'
          />
          <TextField
            select
            label='Category'
            fullWidth
            defaultValue=''
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
          </TextField>

          <Button fullWidth type='submit' variant='contained'>
            Apply Filters
          </Button>
        </Grid2>
      </form>
    </Grid2>
  );
}
