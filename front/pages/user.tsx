import Head from 'next/head';
import { NextPage } from 'next';

import DefaultLayout from '@layout/DefaultLayout';
import UserPage from '@template/UserPage';

const PAGE_TITLE = 'Vartotojo profilis';

const User = () => {
  return (
    <div>
      <Head>
        <title>{PAGE_TITLE}</title>
      </Head>

      <UserPage />
    </div>
  );
};

User.getLayout = (page: NextPage) => {
  return <DefaultLayout title={PAGE_TITLE}>{page}</DefaultLayout>;
};

export default User;
