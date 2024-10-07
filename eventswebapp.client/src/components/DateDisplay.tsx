import { Typography } from '@mui/material';
import React from 'react';

type Props = {
  date: Date;
};

const DateDisplay = ({ date }: Props) => {
  return (
    <Typography>
      {date.toLocaleDateString()} at{' '}
      {date.toLocaleTimeString(undefined, {
        hour: 'numeric',
        minute: '2-digit',
      })}
    </Typography>
  );
};

export default DateDisplay;
