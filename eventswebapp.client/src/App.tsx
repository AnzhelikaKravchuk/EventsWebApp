import { Route, Routes } from 'react-router-dom';
import { ProtectedRoute } from './routes/ProtectedRoute/ProtectedRoute';
import SocialEvents from './routes/SocialEvents/SocialEvents';
import Root from './routes/Root/Root';
import LoginPage from './routes/LoginPage/LoginPage';
import RegisterPage from './routes/RegisterPage/RegisterPage';
import { Role } from './types/types';
import { AuthProvider } from './provider/AuthProvider';
import EventsAdmissions from './routes/EventsAdmissions/EventsAdmissions';
import EventPage from './routes/SocialEvents/EventPage';
import CreateSocialEventPage from './routes/SocialEvents/CreateSocialEventPage';
// const router = createBrowserRouter([
//   {
//     path: '/',
//     element: <Root />,
//     children: [
//       {
//         path: '/socialEvents',
//         element: <SocialEvents />,
//       },
//       {
//         path: '/applications',
//         element: <EventsApplications />,
//       },
//     ],
//   },
// ]);

function App() {
  return (
    <AuthProvider>
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
            path='eventPage'
            element={
              <ProtectedRoute allowedRoles={[Role.User, Role.Admin]}>
                <EventPage />
              </ProtectedRoute>
            }
          />
          <Route
            path='createEvent'
            element={
              <ProtectedRoute allowedRoles={[Role.Admin]}>
                <CreateSocialEventPage />
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
          <Route
            path='admin'
            element={<ProtectedRoute allowedRoles={[Role.Admin]} />}
          />
        </Route>
      </Routes>
    </AuthProvider>
  );
}

export default App;
