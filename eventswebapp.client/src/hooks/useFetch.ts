import { useEffect, useState } from 'react';
import { RetryFetch } from '../services/user';
import { useNavigate } from 'react-router-dom';
import { useAuth } from './useAuth';

export function useFetch<Data>(
  fetch: () => Promise<Data>,
  init: Data | null = null
): [data: Data | null, loading: boolean] {
  const [data, setData] = useState<Data | null>(init);
  const [loading, setLoading] = useState(true);
  const navigate = useNavigate();
  const { logout } = useAuth();

  useEffect(() => {
    function makeFetch() {
      return fetch()
        .then((data) => {
          setData(data);
        })
        .catch(async (error) => {
          if (error.status === 401) {
            await RetryFetch(() => fetch())
              .then((data) => setData(data))
              .catch(() => {
                logout();
                navigate('/login');
              });
          }
        })
        .finally(() => setLoading(false));
    }

    makeFetch();
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  return [data, loading];
}

export function useLazyFetch<Data, FetchArgs extends Array<unknown>>(
  fetch: (...args: FetchArgs) => Promise<Data>,
  init: Data | null = null
): [
  makeFetch: (...args: FetchArgs) => Promise<void>,
  data: Data | null,
  loading: boolean
] {
  const [data, setData] = useState<Data | null>(init);
  const [loading, setLoading] = useState(false);
  const navigate = useNavigate();
  const { logout } = useAuth();

  function makeFetch(...args: FetchArgs) {
    setLoading(true);
    return fetch(...args)
      .then((data) => {
        setData(data);
      })
      .catch(async (error) => {
        if (error.status === 401) {
          await RetryFetch(() => fetch(...args))
            .then((data) => setData(data))
            .catch(() => {
              logout();
              navigate('/login');
            });
        }
      })
      .finally(() => setLoading(false));
  }

  return [makeFetch, data, loading];
}

export function useFetchAction<Response, FetchArgs extends Array<unknown>>(
  fetch: (...args: FetchArgs) => Promise<Response>
): [makeFetch: (...args: FetchArgs) => Promise<Response>, loading: boolean] {
  const [loading, setLoading] = useState(false);
  const navigate = useNavigate();
  const { logout } = useAuth();

  function makeFetch(...args: FetchArgs) {
    setLoading(true);
    return fetch(...args)
      .catch(async (error) => {
        if (error.status === 401) {
          await RetryFetch(() => fetch(...args)).catch(() => {
            logout();
            navigate('/login');
          });
        }
        return error;
      })
      .finally(() => setLoading(false));
  }

  return [makeFetch, loading];
}
