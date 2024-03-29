import {FC} from 'react';

type Props = {
    name?: string,
    width?: string
    height?: string
};

export const LegendIcon: FC<Props> = ({name, width, height}: Props) => {
    if (name === undefined) {
        name = 'Any'
    }
    return (
        <img loading="lazy"
             width={width}
             height={height}
             src={process.env.PUBLIC_URL + `/heroes/${name}.png`}
        />
    );
};