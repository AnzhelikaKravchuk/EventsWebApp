import { Typography } from '@mui/material';

type Props = {
  date: Date;
  hideTime?: boolean;
};

const DateDisplay = ({ date, hideTime }: Props) => {
  return (
    <Typography>
      {date.toLocaleDateString('en-US', {
        day: 'numeric',
        month: 'short',
        year: 'numeric',
      })}
      {hideTime
        ? ' ' +
          date.toLocaleTimeString('en-US', {
            hour: 'numeric',
            minute: '2-digit',
          })
        : null}
    </Typography>
  );
};

export default DateDisplay;
