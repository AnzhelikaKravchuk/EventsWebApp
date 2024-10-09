import { Box, Container, Grid2, Typography } from '@mui/material';

const Error404 = () => {
  return (
    <Container maxWidth='xl' component={'section'} sx={{ mt: 20 }}>
      <Grid2 container flexDirection={'column'} alignItems={'center'}>
        <Box
          component='img'
          src='/404.jpg'
          sx={{ height: '60vh' }}
          title='Image by storyset on Freepik'
        />
        <Typography variant='h3' component='h1' color='primary'>
          Page not found
        </Typography>
      </Grid2>
    </Container>
  );
};

export default Error404;
