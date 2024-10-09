import { Navigate, Route, Routes } from 'react-router-dom';
import { ProtectedRoute } from './routes/ProtectedRoute/ProtectedRoute';
import SocialEvents from './routes/SocialEvents/SocialEvents';
import Root from './routes/Root/Root';
import LoginPage from './routes/LoginPage/LoginPage';
import RegisterPage from './routes/RegisterPage/RegisterPage';
import { Role } from './types/types';
import { AuthProvider } from './provider/AuthProvider';
import EventsAdmissions from './routes/EventsAdmissions/EventsAdmissions';
import EventPage from './routes/SocialEvents/EventPage';
import EditSocialEventPage from './routes/SocialEvents/EditSocialEventPage';
import { CreateAttendeePage } from './routes/EventsAdmissions/CreateAttendeePage';
import MUIThemeProvider from './provider/MUIThemeProvider';
import { LocalizationProvider } from '@mui/x-date-pickers';
import { AdapterDayjs } from '@mui/x-date-pickers/AdapterDayjs';
import Error404 from './routes/Error404/Error404';

function App() {
  return (
    <AuthProvider>
      <LocalizationProvider dateAdapter={AdapterDayjs}>
        <MUIThemeProvider>
          <Routes>
            <Route path='/' element={<Root />}>
              <Route path='login' element={<LoginPage />} />
              <Route path='register' element={<RegisterPage />} />
              <Route
                path='socialEvents'
                element={
                  <ProtectedRoute allowedRoles={[Role.User, Role.Admin]}>
                    <SocialEvents />
                  </ProtectedRoute>
                }
              />
              <Route
                path='eventPage/:id'
                element={
                  <ProtectedRoute allowedRoles={[Role.User, Role.Admin]}>
                    <EventPage />
                  </ProtectedRoute>
                }
              />
              <Route
                path='attendeePage/:id'
                element={
                  <ProtectedRoute allowedRoles={[Role.User, Role.Admin]}>
                    <CreateAttendeePage />
                  </ProtectedRoute>
                }
              />
              <Route
                path='createEvent'
                element={
                  <ProtectedRoute allowedRoles={[Role.Admin]}>
                    <EditSocialEventPage />
                  </ProtectedRoute>
                }
              />
              <Route
                path='editEvent/:id'
                element={
                  <ProtectedRoute allowedRoles={[Role.Admin]}>
                    <EditSocialEventPage isEdit />
                  </ProtectedRoute>
                }
              />
              <Route
                path='admissions'
                element={
                  <ProtectedRoute allowedRoles={[Role.User, Role.Admin]}>
                    <EventsAdmissions />
                  </ProtectedRoute>
                }
              />
              <Route path='*' element={<Navigate to='/404' />} />
              <Route path='404' element={<Error404 />} />
            </Route>
          </Routes>
        </MUIThemeProvider>
      </LocalizationProvider>
    </AuthProvider>
  );
}

export default App;
