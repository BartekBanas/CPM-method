import React, { useRef, useEffect, useState } from 'react';
import * as d3 from 'd3';

const CPMDiagram = () => {
    const svgRef = useRef(null);

    useEffect(() => {
        // Dane dla grafu
        const nodes = [
            { id: 1 },
            { id: 2 },
            { id: 3 },
            { id: 4 },
            { id: 5 }
        ];

        const links = [
            { source: 1, target: 2, label: 'A', duration: 2, earliestStart: 3, earliestFinish: 1, latestStart: 5, latestFinish: 7, reserve: 0 },
            { source: 1, target: 3, label: 'B', duration: 4, earliestStart: 3, earliestFinish: 4, latestStart: 9, latestFinish: 13, reserve: 2 },
            { source: 2, target: 4, label: 'C', duration: 5, earliestStart: 5, earliestFinish: 6, latestStart: 10, latestFinish: 15, reserve: 0 },
            { source: 3, target: 4, label: 'D', duration: 2, earliestStart: 9, earliestFinish: 3, latestStart: 11, latestFinish: 13, reserve: 2 },
            { source: 4, target: 5, label: 'E', duration: 3, earliestStart: 16, earliestFinish: 9, latestStart: 17, latestFinish: 20, reserve: 0 }
        ];

        // Ustawienia dla grafu
        const width = 1500;
        const height = 1000;

        const svg = d3.select(svgRef.current)
            .attr('width', width)
            .attr('height', height);



        const simulation = d3.forceSimulation(nodes)
            .force('link', d3.forceLink(links).id(d => d.id).distance(50))
            .force('charge', d3.forceManyBody().strength(-100))
            .force('center', d3.forceCenter(width / 3, height / 3));

        // Funkcja do obsługi rozpoczęcia przeciągania węzła
        function dragstarted(event, d) {
            if (!event.active) simulation.alphaTarget(0.3).restart();
            d.fx = d.x;
            d.fy = d.y;
        }


        // Funkcja do obsługi przeciągania węzła
        function dragged(event, d) {
            d.fx = event.x;
            d.fy = event.y;
        }

        // Funkcja do obsługi zakończenia przeciągania węzła
        function dragended(event, d) {
            if (!event.active) simulation.alphaTarget(0);
            d.fx = d.x;
            d.fy = d.y;
        }

        // Tworzenie linii
        const link = svg.append('g')
            .attr('class', 'links')
            .selectAll('line')
            .data(links)
            .enter()
            .append('line')
            .attr('stroke', 'black')
            .attr('stroke-width', 2);

        const linkText = svg.append('g')
            .attr('class', 'link-labels')
            .selectAll('text')
            .data(links)
            .enter()
            .append('text')
            .style('font-size', '12px')
            .attr('text-anchor', 'top')
            .attr('alignment-baseline', 'middle')
            .attr('x', d => (d.source.x + d.target.x) / 2)
            .attr('y', d => (d.source.y + d.target.y) / 2);


        // Tworzenie węzłów
        const node = svg.append('g')
            .attr('class', 'nodes')
            .selectAll('circle')
            .data(nodes)
            .enter()
            .append('circle')
            .attr('r', 20)
            .attr('fill', 'lightblue')
            .call(
                d3
                    .drag()
                    .on('start', dragstarted)
                    .on('drag', dragged)
                    .on('end', dragended)
            );

        // Dodanie etykiet do węzłów
        const labels = svg.selectAll('.labels')
            .data(nodes)
            .enter()
            .append('text')
            .attr('class', 'label')
            .attr('text-anchor', 'middle')
            .attr('alignment-baseline', 'middle')
            .text(d => d.id)
            .style('font-size', '14px')
            .style('fill', 'black');

        // Aktualizacja pozycji węzłów i linii na podstawie symulacji
        simulation.on('tick', () => {
            link
                .attr('x1', d => d.source.x)
                .attr('y1', d => d.source.y)
                .attr('x2', d => d.target.x)
                .attr('y2', d => d.target.y);

            linkText
                .attr('x', d => (d.source.x + d.target.x) / 2)
                .attr('y', d => (d.source.y + d.target.y) / 2)

                .text('').append('svg:tspan')
                //.attr('x', d => (d.source.x + d.target.x) / 2)
                .attr('dy', 5)
                .text(d => {
                    const label = d.label;
                    const earliestStart = d.earliestStart;
                    const earliestFinish = d.earliestFinish;
                    return `${label} [${earliestStart}, ${earliestFinish}]`;
                })
                .append('svg:tspan')
                .attr('dx', -32)
                .attr('dy', 15)
                .text(d => {
                    const latestStart = d.latestStart;
                    const latestFinish = d.latestFinish;

                    return `[${latestStart}, ${latestFinish}]`;
                })
                .append('svg:tspan')
                .attr('dx', -50)
                .attr('dy', 15)
                .text(d => {
                    const reserve = d.reserve;
                    return `rezerwa=${reserve}`;
                })
                .append('svg:tspan')
                .attr('dx', -40)
                .attr('dy', 20)
                .text(d => {
                    const duration = d.duration;
                    return `${duration}`;
                });

            node
                .attr('cx', d => d.x)
                .attr('cy', d => d.y);

            labels
                .attr('x', d => d.x)
                .attr('y', d => d.y);
        });
    }, []);

    return (
        <svg ref={svgRef}>
        </svg>
    );
};

export default CPMDiagram;
