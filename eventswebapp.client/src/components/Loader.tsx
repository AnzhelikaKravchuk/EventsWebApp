import { CircularProgress, Container } from '@mui/material';

type Props = {
  fullPage?: boolean;
};

const Loader = ({ fullPage }: Props) => {
  return (
    <Container
      maxWidth='xl'
      sx={
        fullPage
          ? {
              mt: 20,
              display: 'flex',
              justifyContent: 'center',
              alignItems: 'center',
              height: '50vh',
            }
          : {
              display: 'flex',
              justifyContent: 'center',
              alignItems: 'center',
            }
      }>
      <CircularProgress />
    </Container>
  );
};

export default Loader;
