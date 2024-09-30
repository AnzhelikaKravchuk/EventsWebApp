import requests from './axiosInstance';

const SocialEvents = {
  getSocialEventsList: (params: URLSearchParams, cancellationToken: any) =>
    requests.get('socialEvents', params, cancellationToken),
};

export default SocialEvents;
