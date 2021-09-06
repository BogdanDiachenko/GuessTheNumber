import { observer } from 'mobx-react-lite';
import React from 'react';
import Calendar from 'react-calendar';
import { Header, Menu } from 'semantic-ui-react';
import { useStore } from '../../../app/stores/store';

export default observer(function HistoryFilters() {
    const {historyStore: {predicate, setPredicate}} = useStore();
    return (
        <>
            <Menu vertical size='large' style={{ width: '100%', marginTop: 25 }}>
                <Header icon='filter' attached color='teal' content='Filters' />
                <Menu.Item 
                    content='All Games' 
                    active={predicate.has('all')}
                    onClick={() => setPredicate('all', 'true')}
                />
                <Menu.Item 
                    content="I played" 
                    active={predicate.has('isParticipiant')}
                    onClick={() => setPredicate('isParticipiant', 'true')}
                />
                <Menu.Item 
                    content="I hosted" 
                    active={predicate.has('isHost')}
                    onClick={() => setPredicate('isHost', 'true')}
                />
                <Menu.Item
                    content="I won"
                    active={predicate.has('isWinner')}
                    onClick={() => setPredicate('isWinner', 'true')}
                />
            </Menu>
            <Header />
            <Calendar 
                onChange={(date : Date) => setPredicate('startDate', date as Date)}
                value={predicate.get('startDate') || new Date()}
            />
        </>
    )
})