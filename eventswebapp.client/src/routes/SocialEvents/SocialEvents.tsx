import { useEffect, useState } from 'react';
import { Role } from '../../types/types';
import { GetSocialEvents } from '../../services/socialEvents';
import Items from '../../components/Items/Items';
import Filters from '../../components/Items/Filters';
import { useAuth } from '../../hooks/useAuth';
import {
  Box,
  Button,
  Container,
  Drawer,
  Grid2,
  Pagination,
  Toolbar,
} from '@mui/material';
import Loader from '../../components/Loader';
import { useLazyFetch } from '../../hooks/useFetch';

const drawerWidth = 500;
const pageSize = 8;

const SocialEvents = () => {
  const { role } = useAuth();
  const [filters, setFilters] = useState(new FormData());
  const [pageIndex, setPageIndex] = useState(1);
  const [requestEvents, events, eventsLoading] = useLazyFetch(GetSocialEvents);

  useEffect(() => {
    requestEvents(filters, pageIndex, pageSize);
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [pageIndex, filters]);

  const handlePageClick = (_: unknown, value: number) => {
    setPageIndex(value);
  };

  return (
    <Grid2
      container
      direction={'column'}
      justifyContent={'space-between'}
      alignItems={'center'}
      sx={{ height: '90vh' }}>
      <Container maxWidth='xl'>
        {!eventsLoading || !events ? (
          <Items currentItems={events?.items ?? []} drawerWidth={drawerWidth} />
        ) : (
          <Loader fullPage />
        )}
        <Drawer
          variant='permanent'
          anchor='right'
          sx={{
            width: drawerWidth,
            '& .MuiDrawer-paper': {
              width: drawerWidth,
              boxSizing: 'border-box',
            },
          }}>
          <Toolbar />
          <Filters
            pageSize={pageSize}
            setFilters={setFilters}
            setPageIndex={setPageIndex}
          />
          {role === Role.Admin && (
            <Box
              border={2.5}
              borderColor={(theme) => theme.palette.primary.light}
              borderRadius={2}
              padding={2}
              m={3}>
              <Button variant='contained' href='/createEvent' fullWidth>
                Create New Event
              </Button>
            </Box>
          )}
        </Drawer>
      </Container>
      <Pagination
        count={events?.totalPages}
        onChange={handlePageClick}
        sx={{ mr: 50 }}
      />
    </Grid2>
  );
};

export default SocialEvents;
