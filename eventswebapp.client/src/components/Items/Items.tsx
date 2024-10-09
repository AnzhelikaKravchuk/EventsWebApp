import { SocialEventModel } from '../../types/types';
import {
  ButtonBase,
  Card,
  CardContent,
  CardMedia,
  Chip,
  Grid2,
  Typography,
} from '@mui/material';
import DateDisplay from '../DateDisplay';

interface Props {
  currentItems: Array<SocialEventModel>;
  drawerWidth: string | number;
}
export default function Items({ currentItems }: Props) {
  return (
    <Grid2 container gap={2} flexDirection={'row'} wrap='wrap' sx={{ mt: 20 }}>
      {currentItems &&
        currentItems.reverse().map((item) => (
          <Card
            key={item.id}
            component='article'
            sx={{ width: 550, height: 140 }}>
            <ButtonBase
              href={`/eventPage/${item.id}`}
              sx={{
                height: '100%',
                width: '100%',
                justifyContent: 'space-between',
              }}>
              <CardContent>
                <Grid2
                  container
                  direction={'column'}
                  gap={2}
                  alignItems={'flex-start'}>
                  <Grid2>
                    <Typography variant='h6' sx={{ fontWeight: 'bold' }}>
                      {item.eventName}
                    </Typography>
                    <DateDisplay date={new Date(item.date)} />
                  </Grid2>
                  <Chip label={item.category} />
                </Grid2>
              </CardContent>
              {item.image && (
                <CardMedia
                  component='img'
                  image={`${import.meta.env.VITE_IMAGES_HOST}/${item.image}`}
                  alt={`${item.eventName} - cover`}
                  title={`${item.eventName} - cover`}
                  sx={{ width: 140, height: 140, objectFit: 'cover' }}
                />
              )}
            </ButtonBase>
          </Card>
        ))}
    </Grid2>
  );
}
